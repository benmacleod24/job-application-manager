import {
	deleteApplication,
	getApplications,
} from "@/lib/helpers/applications";
import { useQuery } from "@tanstack/react-query";
import { Loader } from "lucide-react";
import Application from "./Application";
import { useEffect, useMemo, useState } from "react";
import { Input } from "../ui/input";
import { ScrollArea } from "../ui/scroll-area";
import ActiveApplication from "./ActiveApplication";
import CreateApplication from "./CreateApplication";

export default function ApplicationsWrapper(props: {}) {
	const [activeApplicationIndex, setActiveApplicationIndex] =
		useState<number>(-1);
	const [search, setSearch] = useState<string>("");

	const { data, isLoading } = useQuery({
		queryKey: ["applications"],
		queryFn: getApplications,
	});

	useEffect(() => {
		if (activeApplicationIndex < 0 && data && data.length >= 1) {
			setActiveApplicationIndex(0);
		}
	}, [data]);

	const displayedApplications = useMemo(() => {
		if (!data) return [];
		// Search is empty only return top 5.
		if (!search) {
			return data?.slice(0, 5);
		}

		return data.filter(
			(a) =>
				a.name?.toLowerCase().includes(search.toLowerCase()) ||
				a.description?.toLowerCase().includes(search.toLowerCase()) ||
				a.resume?.name.toLowerCase().includes(search.toLowerCase()) ||
				a.jobUrl?.toLowerCase().includes(search.toLowerCase())
		);
	}, [data, search]);

	async function handleApplicationDelete() {
		if (!data) return;

		await deleteApplication(data[activeApplicationIndex].id);

		if (data.length - 1 === activeApplicationIndex) {
			setActiveApplicationIndex(data.length - 2);
		}
	}

	return (
		<div className="grid grid-cols-2 w-full gap-4">
			<div className="w-full">
				<div className="flex items-end justify-between pr-3 mb-4">
					<div>
						<h1 className="font-semibold text-lg">Applications</h1>
						<p className="text-sm text-muted-foreground">
							A list of all applications submitted.
						</p>
					</div>
					<div className="flex items-center gap-3">
						<Input
							value={search}
							onChange={(e) => setSearch(e.target.value)}
							className="w-3/3"
							placeholder="Search Applications"
						/>
						<CreateApplication />
					</div>
				</div>
				<div className="flex justify-center w-full">
					{isLoading && <Loader className="animate-spin" />}
					{data && (
						<ScrollArea className="h-[720px] pr-3 w-full">
							{displayedApplications.map((a, i) => (
								<Application
									key={i}
									application={a}
									isActiveApplication={
										activeApplicationIndex === i
									}
									setActiveApplicationIndex={() =>
										setActiveApplicationIndex(i)
									}
								/>
							))}
						</ScrollArea>
					)}
				</div>
			</div>

			{data && activeApplicationIndex >= 0 && (
				<ActiveApplication
					application={data[activeApplicationIndex]}
					handleDeleteApplication={handleApplicationDelete}
				/>
			)}
		</div>
	);
}
