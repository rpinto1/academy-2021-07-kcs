import React, {useState, useEffect} from 'react'
import TitlesList from './TitlesList';
//test 
//import {db} from './test-db/db.json';

export default function GainLoseFetcher(props) {

    const { gainLoseURL } = props;
    
    const [allData, setAllData] = useState({});

    const fetchData = async (url) => {

       await fetch(url)
        .then(response => response.json())
        .then(data => setAllData(data["result"]))
        .catch(err => console.error("Error Reading data " + err));
          
    };

 
    useEffect(() => {
        console.log(fetchData(gainLoseURL))
        setAllData(fetchData(gainLoseURL));
    }, [gainLoseURL]);


    return (
        <main className="App">
            <section className='gainlose-data' >
                <TitlesList className='gain-items' data={allData.gainers} />
                <TitlesList className='lose-items' data={allData.losers} /> 
            </section>
        </main>
    );

}
