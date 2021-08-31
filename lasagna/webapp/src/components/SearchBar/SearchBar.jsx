import React, { useState, useEffect } from 'react';
import NameTicker from './NameTicker';

function SearchBar() {
    const [nameTickers, setNameTicker] = useState([]);
    const [pattern, setPattern] = useState('');

    useEffect(() => {
        fetch("http://localhost:3010/api/Companies/search/"+ pattern + "/0").then(result => {
            if (result.status != 200) {
                console.log("error");
                return;
            }
            result.json().then(data => {
                if (data != null) {
                    console.log(data);
                    setNameTicker(data.result);
                }
            })
        })
    }, [pattern]);

    return (
        <div className="SearchBar">
            
            <header className="Search-header">
                <input type="text" className="input" value={pattern} onChange={test => setPattern(test.target.value)} />
            </header>
            
            <div className="NameTickers">
                {
                    (nameTickers != null) ? nameTickers.map((nameTicker, index) => <NameTicker key={ index } nameTicker={ nameTicker } />) : ''
                }
            </div>
        </div>
    );
}

export default SearchBar;
