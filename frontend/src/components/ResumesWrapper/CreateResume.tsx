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
import { Loader, Plus } from "lucide-react";
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
import { useState } from "react";
import { createResume } from "@/lib/helpers/resume";

const formSchema = z.object({
	name: z
		.string({ required_error: "Name can not be empty." })
		.min(1, "Name can not be empty."),
});

export default function CreateResume() {
	const [open, setOpen] = useState<boolean>(false);

	const form = useForm<z.infer<typeof formSchema>>({
		resolver: zodResolver(formSchema),
	});

	async function onSubmit(values: z.infer<typeof formSchema>) {
		if (await createResume(values.name)) {
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
				<Button size={"sm"} variant={"secondary"}>
					<Plus /> Resume
				</Button>
			</DialogTrigger>
			<DialogContent>
				<Form {...form}>
					<form
						onSubmit={form.handleSubmit(onSubmit)}
						className="grid gap-4"
					>
						<DialogHeader>
							<DialogTitle>Create Resume</DialogTitle>
							<DialogDescription>
								You are about to create a new resume for job
								applications.
							</DialogDescription>
						</DialogHeader>
						<div>
							<FormField
								control={form.control}
								name="name"
								render={({ field }) => (
									<FormItem>
										<FormLabel>Resume Name</FormLabel>
										<FormControl>
											<Input {...field} />
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
									<Plus />
								)}
								Create
							</Button>
						</DialogFooter>
					</form>
				</Form>
			</DialogContent>
		</Dialog>
	);
}
