import { queryClient } from "@/App";
import { Resume } from "@/types";

export async function getResumes() {
	return (await fetch("https://localhost:7085/api/Resume", {
		method: "GET",
	}).then((r) => r.json())) as Resume[];
}

export async function deleteResume(id: number) {
	const resp = await fetch(`https://localhost:7085/api/Resume/${id}`, {
		method: "DELETE",
	});

	// The response from the server was good, invalidate current resume cache.
	if (resp.ok) {
		queryClient.refetchQueries({ queryKey: ["resumes", "applications"] });
	}
}

export async function createResume(name: string) {
	const resp = await fetch(`https://localhost:7085/api/Resume`, {
		method: "POST",
		headers: {
			["Content-Type"]: "application/json",
		},
		body: JSON.stringify({
			name,
		}),
	});

	if (resp.ok) {
		queryClient.invalidateQueries({ queryKey: ["resumes"] });
		return true;
	}

	return false;
}
