import { Application as TApplication } from "@/types";
import { Button } from "../ui/button";
import { Link, SquareArrowOutUpRight } from "lucide-react";
import { Badge, badgeVariants } from "@/components/ui/badge";
import { cn } from "@/lib/utils";

export default function Application(props: {
	application: TApplication;
	isActiveApplication: boolean;
	setActiveApplicationIndex: () => void;
}) {
	return (
		<div
			className={cn(
				"w-full rounded-md border grid gap-2 p-4 my-2 first:mt-0 hover:bg-zinc-900 transition-colors cursor-pointer",
				props.isActiveApplication && "bg-zinc-900"
			)}
			onClick={props.setActiveApplicationIndex}
		>
			{/* header */}
			<div className="flex justify-between">
				<h1 className="text-sm font-semibold leading-3 capitalize">
					{props.application.name}
				</h1>
			</div>
			{/* content */}
			<div>
				<p className="text-sm text-muted-foreground line-clamp-2">
					{props.application.description}
				</p>
			</div>
			{/* footer */}
			<div className="flex items-center gap-2">
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
		</div>
	);
}
