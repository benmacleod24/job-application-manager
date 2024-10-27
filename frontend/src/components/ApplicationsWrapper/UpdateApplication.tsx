import {
	Dialog,
	DialogClose,
	DialogContent,
	DialogDescription,
	DialogFooter,
	DialogHeader,
	DialogTitle,
	DialogTrigger,
} from "@/components/ui/dialog";
import { Button } from "../ui/button";
import { FilePenLine, Loader, Plus } from "lucide-react";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
	Form,
	FormControl,
	FormField,
	FormItem,
	FormLabel,
	FormMessage,
} from "@/components/ui/form";
import { Input } from "../ui/input";
import { useEffect, useState } from "react";
import { Textarea } from "../ui/textarea";
import {
	Select,
	SelectContent,
	SelectItem,
	SelectTrigger,
	SelectValue,
} from "../ui/select";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { getResumes } from "@/lib/helpers/resume";
import { updateApplication } from "@/lib/helpers/applications";
import { Application } from "@/types";

const formSchema = z.object({
	name: z
		.string({ required_error: "Name can not be empty." })
		.min(1, "Name can not be empty."),
	jobLink: z.ostring(),
	description: z.ostring(),
	resumeId: z.string({ required_error: "Resume can not be empty." }),
});

export default function UpdateApplication(props: {
	application: Application;
}) {
	const [open, setOpen] = useState<boolean>(false);

	const { data } = useQuery({
		queryKey: ["resumes"],
		queryFn: getResumes,
	});

	const form = useForm<z.infer<typeof formSchema>>({
		resolver: zodResolver(formSchema),
		defaultValues: {
			jobLink: "",
			description: "",
		},
	});

	useEffect(() => {
		if (props.application) {
			form.reset({
				description: props.application.description,
				jobLink: props.application.jobUrl,
				name: props.application.name,
				resumeId: props.application.resume?.id.toString() || "0",
			});
		}
	}, [props.application]);

	async function onSubmit(values: z.infer<typeof formSchema>) {
		const resp = await updateApplication(props.application.id, {
			description: values.description || "",
			name: values.name,
			resumeId: Number(values.resumeId),
			jobLink: values.jobLink || "",
		});

		if (resp) {
			form.reset();
			setOpen(false);
		}
	}

	return (
		<Dialog
			open={open}
			onOpenChange={(v) => {
				setOpen(v);
				form.reset();
			}}
		>
			<DialogTrigger asChild>
				<Button variant={"ghost"} size={"icon"}>
					<FilePenLine />
				</Button>
			</DialogTrigger>
			<DialogContent>
				<Form {...form}>
					<form
						onSubmit={form.handleSubmit(onSubmit)}
						className="grid gap-4"
					>
						<DialogHeader>
							<DialogTitle>Create Application</DialogTitle>
							<DialogDescription>
								You are about to create a new resume for job
								applications.
							</DialogDescription>
						</DialogHeader>
						<div className="space-y-3">
							<FormField
								control={form.control}
								name="name"
								render={({ field }) => (
									<FormItem>
										<FormLabel>Name</FormLabel>
										<FormControl>
											<Input {...field} />
										</FormControl>

										<FormMessage />
									</FormItem>
								)}
							/>
							<FormField
								control={form.control}
								name="resumeId"
								render={({ field }) => (
									<FormItem>
										<FormLabel>Resume Used</FormLabel>
										<Select
											onValueChange={field.onChange}
											defaultValue={field.value}
										>
											<FormControl>
												<SelectTrigger>
													<SelectValue placeholder="Select a Resume" />
												</SelectTrigger>
											</FormControl>
											<SelectContent>
												{data &&
													data.map((r) => (
														<SelectItem
															key={r.id}
															value={r.id.toString()}
														>
															{r.name}
														</SelectItem>
													))}
											</SelectContent>
										</Select>

										<FormMessage />
									</FormItem>
								)}
							/>
							<FormField
								control={form.control}
								name="jobLink"
								render={({ field }) => (
									<FormItem>
										<FormLabel>Posting Link</FormLabel>
										<FormControl>
											<Input {...field} />
										</FormControl>

										<FormMessage />
									</FormItem>
								)}
							/>
							<FormField
								control={form.control}
								name="description"
								render={({ field }) => (
									<FormItem>
										<FormLabel>Description</FormLabel>
										<FormControl>
											<Textarea {...field} />
										</FormControl>

										<FormMessage />
									</FormItem>
								)}
							/>
						</div>
						<DialogFooter>
							<DialogClose asChild>
								<Button
									variant={"destructive"}
									onClick={() => {
										form.reset();
									}}
								>
									Cancel
								</Button>
							</DialogClose>
							<Button>
								{form.formState.isSubmitting ? (
									<Loader className="animate-spin" />
								) : (
									<FilePenLine />
								)}
								Update
							</Button>
						</DialogFooter>
					</form>
				</Form>
			</DialogContent>
		</Dialog>
	);
}
