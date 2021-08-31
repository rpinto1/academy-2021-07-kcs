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

            <div class="ui search">
                <div class="ui icon input">
                    <input class="prompt"
                        type="text"
                        placeholder="Search for a company"
                        value={pattern}
                        onChange={test => setPattern(test.target.value)} />
                        <i class="search icon"></i> 
                </div>
                <div class="results">       
                    {
                        (nameTickers != null) ? nameTickers.map((nameTicker, index) => <NameTicker key={ index } nameTicker={ nameTicker } />) : ''
                    }
                </div>
            </div>
        </div>
    );
}

export default SearchBar;
