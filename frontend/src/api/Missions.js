
export async function getMissions() {
    const response = await fetch("http://localhost:5142/api/mission");

    if (!response.ok) {
        throw new Error("Failed to fetch missions");
    }

    return response.json();
}