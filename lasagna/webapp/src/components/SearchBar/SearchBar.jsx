﻿import React, { useState, useEffect } from 'react';
import NameTicker from './NameTicker';
import $ from 'jquery';

function SearchBar() {
    const [nameTickers, setNameTicker] = useState([]);
    const [pattern, setPattern] = useState('');
    let pageIndex = 0;

    function indexPlus() {
        alert(pageIndex);
        pageIndex++;
    }
    function indexMinus() {
        alert(pageIndex);
        if (pageIndex != 0) { pageIndex--;}
    }

    useEffect(() => {
        fetch("http://localhost:3010/api/Companies/search/"+ pattern +"/"+ pageIndex).then(result => {
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

    useEffect(() => {
        if(pattern == '') {
            $(".search_list").hide();
        } else {
            $(".search_list").show();
        }
    }, [pattern]);
    

    return (
        <div className="SearchBar">

            <div className="ui search">
                <div className="ui icon input">
                    <input className="prompt"
                        type="text"
                        placeholder="Search for a company"
                        value={pattern}
                        onChange={test => setPattern(test.target.value)} />
                        <i class="search icon"></i> 
                </div>~
                <div className= "search_list">
                <div className="ui raised fluid text segment">       
                    {    nameTickers &&
                         nameTickers.map((nameTicker, index) => <div className='results'><p><NameTicker key={ index } nameTicker={ nameTicker } /></p></div>) 
                    }
                </div>
                </div>
            </div>
            <button onClick={indexPlus}>+</button>;
            <button onClick={indexMinus}>-</button>;
        </div>
    );
}

export default SearchBar;
