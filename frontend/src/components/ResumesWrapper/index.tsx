import { useQuery } from "@tanstack/react-query";
import ResumeTable from "./ResumeTable";
import { Loader } from "lucide-react";
import CreateResume from "./CreateResume";
import { getResumes } from "@/lib/helpers/resume";

export default function ResumesWrapper() {
	const { data, isLoading } = useQuery({
		queryKey: ["resumes"],
		queryFn: getResumes,
	});

	return (
		<div className=" w-full space-y-4">
			<div className="flex items-end justify-between w-full">
				<div>
					<h1 className="font-semibold text-lg">Resumes</h1>
					<p className="text-xs text-muted-foreground">
						A list of all resumes submitted alongside job
						applications.
					</p>
				</div>
				<CreateResume />
			</div>
			<div className="flex justify-center w-full">
				{isLoading && <Loader className="animate-spin" />}
				{data && <ResumeTable resumes={data} />}
			</div>
		</div>
	);
}
