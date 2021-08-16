import React, {useState, useEffect} from 'react'
import TitlesList from './TitlesList';
//test 
//import {db} from './test-db/db.json';

export default function GainLoseFetcher(props) {

    const options = props.config;
    const urlSet = props.urlSet;

    
    const [gainersData, setGainersData] = useState({});
    const [losersData, setLosersData] = useState({});


    const fetchData = async (url, setterFunc) => {

        let response = await fetch(url, options)
            .catch(err => console.error(err));

        response = await response.json()
        setterFunc(response);

    };


    useEffect(() => {
        fetchData(urlSet.gainersUrl, setGainersData);
        fetchData(urlSet.losersUrl, setLosersData);
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
