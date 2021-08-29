import React, {useState, useEffect} from 'react'
import TitlesList from './TitlesList';
//test 
//import {db} from './test-db/db.json';

export default function GainLoseFetcher(props) {

    const {url} = props;
    
    const [allData, setAllData] = useState({});

    const fetchData = async endpoint => {
        
        let response = await fetch(endpoint)
            .catch(err => console.error(err));

        response = await response.json();
        setAllData(response);
        
    };

 
    useEffect(() => {
        fetchData(url);
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
