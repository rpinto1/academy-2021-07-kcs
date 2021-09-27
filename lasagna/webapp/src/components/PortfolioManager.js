import { userId } from "./UserManager";


 export const urlGetPortofolios = `http://localhost:3010/api/Portfolios/portfolio?userId=${userId}` 

 export const fetchAndSetPortfolio = (url, setterFunc) => {

    (async function () {
        try {
            const response = await fetch(url)

            const data = await response.json();

            if (data != null) {
                setterFunc(() => data.result);
                console.log("fetched data: ", data.result)
            }

        } catch (e) {
            console.error("Error while fetching: ", e);
        }
    })()
};

