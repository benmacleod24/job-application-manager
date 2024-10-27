import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import ResumesWrapper from "./components/ResumesWrapper";
import ApplicationsWrapper from "./components/ApplicationsWrapper";

export const queryClient = new QueryClient({
	defaultOptions: {
		queries: {
			refetchOnWindowFocus: true,
		},
	},
});

function App() {
	return (
		<QueryClientProvider client={queryClient}>
			<div className="w-screen flex gap-10 p-10">
				<div className="w-1/3 ">
					<ResumesWrapper />
				</div>

				<div className="w-2/3">
					<ApplicationsWrapper />
				</div>
			</div>
		</QueryClientProvider>
	);
}

export default App;
