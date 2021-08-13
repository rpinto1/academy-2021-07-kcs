import React, {useState, useEffect} from 'react'
import TitlesList from './TitlesList';

export default function GainLoseFetcher() {
   
    const [gainersData, setGainersData] = useState({});
    const [losersData, setLosersData] = useState({});

    const fetchData = async (url, setterFunc) => {

        let response = await fetch(url, {
            "method": "GET",
            "headers": {
                "x-rapidapi-key": "c16e3abf67msh98c7e364d5eed9ep19f991jsnd7123a9386c2",
                "x-rapidapi-host": "yahoo-finance15.p.rapidapi.com"
            },

        }).catch(err => console.error(err));

        setterFunc(await response.json());

    };


    const gainersUrl = "https://yahoo-finance15.p.rapidapi.com/api/yahoo/co/collections/day_gainers?start=0";
    const losersUrl = "https://yahoo-finance15.p.rapidapi.com/api/yahoo/co/collections/day_losers?start=0";
    //const localGainersUrl = "http://localhost:3002/gainers";
    //const localLosersUrl = "http://localhost:3002/losers";


    useEffect(() => {
        fetchData(gainersUrl, setGainersData);
        fetchData(losersUrl, setLosersData);

    }, []);


    return (
        <main className="App">
            <section className='gainlose-data' >
                <TitlesList className='gain-items' quotes={gainersData} />
                <TitlesList className='lose-items' quotes={losersData} />
            </section>


        </main>
    );

}
