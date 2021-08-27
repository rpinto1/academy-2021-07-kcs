import React, {useState, useEffect} from 'react'
import TitlesList from './TitlesList';
//test 
//import {db} from './test-db/db.json';

export default function GainLoseFetcher(props) {

    const {gainLoseUrl} = props;
    
    const [allData, setAllData] = useState({});


    const fetchData = async (url, setterFunc) => {
        console.log("fetchdata")
        
        let response = await fetch(url)
            .catch(err => console.error(err));

        response = await response.json();
        setterFunc(response);
        console.log("RESPONSE: ", response)

    };

 
    useEffect(() => {
        fetchData(gainLoseUrl, setAllData);
        console.log("useEffect: ", allData)
    }, []);


    return (
        <main className="App">
            <section className='gainlose-data' >
                <TitlesList className='gain-items' quotes={allData.gainers} />
                <TitlesList className='lose-items' quotes={allData.losers} /> 
            </section>
        </main>
    );

}
