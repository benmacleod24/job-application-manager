export type Resume = {
	id: number;
	name: string;
	applications?: any;
};

export type Application = {
	id: number;
	name?: string;
	description?: string;
	jobUrl?: string;
	resume?: Resume;
};

export type CreateUpdateApplicationDto = {
	name: string;
	description: string;
	jobLink: string;
	resumeId: number;
};
