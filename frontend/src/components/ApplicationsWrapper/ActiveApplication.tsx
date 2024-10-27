import { Application } from "@/types";
import { badgeVariants, Badge } from "../ui/badge";
import { SquareArrowOutUpRight, Trash2 } from "lucide-react";
import { Separator } from "../ui/separator";
import { Button } from "../ui/button";
import UpdateApplication from "./UpdateApplication";

export default function ActiveApplication(props: {
	application: Application;
	handleDeleteApplication: () => void;
}) {
	return (
		<div className="w-full h-full border rounded-md overflow-hidden p-5">
			<div className="flex items-center justify-between">
				<h1 className="font-semibold capitalize">
					{props.application.name}
				</h1>
				<div className="space-x-1">
					<UpdateApplication application={props.application} />
					<Button
						variant={"ghost"}
						size={"icon"}
						className="hover:text-red-400"
						onClick={props.handleDeleteApplication}
					>
						<Trash2 />
					</Button>
				</div>
			</div>
			<div className="flex items-center gap-2 mt-2">
				{props.application.jobUrl && (
					<a
						href={props.application.jobUrl}
						target="_blank"
						className={badgeVariants({
							variant: "secondary",
							className: "drop-shadow flex items-center gap-1",
						})}
					>
						<SquareArrowOutUpRight className="w-3 h-3 mt-0.5" />
						Job Posting
					</a>
				)}
				{props.application.resume && (
					<Badge variant={"secondary"} className="drop-shadow">
						{props.application.resume.name}
					</Badge>
				)}
			</div>
			<Separator className="my-4 mb-3" />

			<div>
				<p className="whitespace-break-spaces">
					{props.application.description}
				</p>
			</div>
		</div>
	);
}
