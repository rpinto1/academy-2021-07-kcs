import React, { useState, useEffect } from 'react';
import NameTicker from './NameTicker';
import $ from 'jquery';

function SearchBar() {
    const [nameTickers, setNameTicker] = useState([]);
    const [pattern, setPattern] = useState('');
    let [pageIndex, setPageIndex] = useState(0);


    {/*function description(nameTicker.ticker){
       fetch("http://localhost:3010/api/Company/"+ ticker +"/description).then(result.json() => {
            setDescription(data.result);
            }}
     */}

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
    }, [pattern,pageIndex]);

    useEffect(() => {
        if(pattern == '') {
            $("#search_list").hide();
        } else {
            $("#search_list").show();
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
                        onChange={test => setPattern(test.target.value), setPageIndex(0)} />
                        <i class="search icon"></i> 
                </div>
              
                <div className="ui raised fluid text segment" id="search_list">       
                    {    nameTickers &&
                         nameTickers.map((nameTicker, index) => <NameTicker key={ index } nameTicker={ nameTicker } />) 
                    }
                </div>
            </div>
            <button onClick={()=>setPageIndex(prevState => prevState+1)}>+</button>;
            <button onClick={() => setPageIndex(prevState => prevState-1)}>-</button>;
        </div>
    );
}

export default SearchBar;
