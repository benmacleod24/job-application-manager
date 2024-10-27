import { queryClient } from "@/App";
import { Application, CreateUpdateApplicationDto } from "@/types";

export async function getApplications() {
	return (await fetch("https://localhost:7085/api/Applications", {
		method: "GET",
	}).then((r) => r.json())) as Application[];
}

export async function deleteApplication(id: number) {
	const resp = await fetch(`https://localhost:7085/api/Applications/${id}`, {
		method: "DELETE",
	});

	// The response from the server was good, invalidate current resume cache.
	if (resp.ok) {
		queryClient.refetchQueries({ queryKey: ["applications"] });
	}
}
export async function createApplication(data: CreateUpdateApplicationDto) {
	const resp = await fetch(`https://localhost:7085/api/Applications`, {
		method: "POST",
		headers: {
			["Content-Type"]: "application/json",
		},
		body: JSON.stringify(data),
	});

	if (resp.ok) {
		queryClient.invalidateQueries({ queryKey: ["applications"] });
		return true;
	}

	return false;
}
