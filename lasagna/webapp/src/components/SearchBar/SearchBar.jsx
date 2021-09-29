import React, { useState, useEffect } from 'react';
import NameTicker from './NameTicker';
import $ from 'jquery';
import { Button } from 'semantic-ui-react';


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
                    setNameTicker(data.result);
                    console.log(data.result);
                }
            })
        })
    }, [pattern,pageIndex]);

    useEffect(() => {
        // var val = Document.getElementById('search_list').value;
        // if (r) {
        //  window.location.href  = "/about/" + val;
        // }    
        if(pattern == '' ) {
            $("#search_list").hide();
        } else {
            $("#search_list").show();
        }
    }, [pattern]);

    const handleSearchBar = () => {
            $("#search_list").show();
    };

    


    return (
        <div className="SearchBar">

            <div className="ui search">
                <div className="ui icon input">
                    <input className="prompt"
                        type="text"
                        placeholder="Search for a company"
                        value={pattern}
                        onChange={(test) => { setPattern(test.target.value); setPageIndex(0);  }} 
                        />
                        <i className="search icon"></i> 
                </div>

                {    (nameTickers.length > 0) &&

                (<div className="ui raised fluid text segment" 
                //please, do not erase id. thx!
                            id='search_list' 
                >       
                    
                
                       { 
                       nameTickers.map((nameTicker, index) => <NameTicker key={index} nameTicker={nameTicker} />)}

                        {console.log("aqui"+nameTickers.length)}
                        {
                            pageIndex > 0 &&(
                                <Button circular icon='arrow left' size='tiny' onClick={() => { setPageIndex(prevState => prevState - 1) }}/>
                            )
                        }
                        {
                            nameTickers.length === 30 &&(
                                <Button circular icon='arrow right' size='tiny' circular onClick={() => setPageIndex(prevState => prevState + 1)}/>
                            )
                        }
                        {/* <Button circular icon='arrow left' size='tiny' onClick={() => { if (pageIndex > 0) { setPageIndex(prevState => prevState - 1) } }}/>
                        <Button circular icon='arrow right' size='tiny' circular onClick={() => setPageIndex(prevState => prevState + 1)}/> */}
 
                    
                </div>)
                    
                }
            </div>
        </div>
    );
}

export default SearchBar;
