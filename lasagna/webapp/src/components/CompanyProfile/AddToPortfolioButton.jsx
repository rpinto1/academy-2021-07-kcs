import React, {useState,useEffect} from 'react';
import axios from 'axios';
import { Button, Icon, Dropdown } from 'semantic-ui-react';
import { headersWithoutCookies } from '../UserManager';
import { urlGetPortofolios } from '../PortfolioManager';
import Portfolio from './Portfolio';
import $ from 'jquery';
import { userId } from '../UserManager';


export default function AddToPortfolioButton({ticker}) {

  const [clicked, setClicked] = useState(true);
    const [portfolios, setPortfolios] = useState([])

    useEffect(() => axios.get(urlGetPortofolios, headersWithoutCookies)
    .then(res => {
            setPortfolios(res.data.result)
    })
    .catch(error => console.log(error)), [])


    const handleClick = () => {
        if(userId) {
            setClicked(!clicked)
        } 
               /* render(() => <Dropdown floating options={[{key: "", text: "You need to be logged in first", value: ""}]} text='Profile' /> )*/
            
     
    };

    useEffect(() => {
        $("#portfolio_list").toggle()
    }, [clicked])  

    return (
         <>
       <div class="ui inline floating dropdown">
            <Button 
            icon labelPosition ='left' 
            size='tiny' 
            floated='right'
            onClick = {handleClick}>
                <span> Add to your portfolio</span>
                                            
        {
            clicked &&           
            <Icon 
                name='heart' 
                color='pink'/>          
        }
            <Icon 
                name='heart outline' 
                color='black'/>
            </Button>
            </div>
    
        {    portfolios.length > 0 &&

                
                (<div className="ui raised fluid text segment" id="portfolio_list" > 
                <div class="menu">
                    {/* <div class="ui icon search input">
                        <i class="search icon"></i>
                    <input type="text" placeholder="Search Portfolio..."></input>
                    </div> */}
                    <div class="menu">
                    <div class="header">
                        <h5> <i className = "suitcase icon" ></i> 
                        Portfolios </h5>
                    </div> 
                    <div class="divider"></div>        
    
                { 
                    portfolios.map((portfolio, index) => 
                    <div className= 'item'> <Portfolio key={index} portfolio={portfolio} ticker={ticker} /></div>
                    )
       
                }

                </div>
                </div>
                </div>)

        }
         
        </>
 
    
    )
}
