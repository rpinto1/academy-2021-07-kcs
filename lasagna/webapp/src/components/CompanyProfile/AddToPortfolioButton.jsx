import React, {useState,useEffect} from 'react';
import axios from 'axios';
import { Button, Icon } from 'semantic-ui-react';
import { headersWithoutCookies } from '../UserManager';
import { urlGetPortofolios } from '../PortfolioManager';
import Portfolio from './Portfolio';
import $ from 'jquery';


export default function AddToPortfolioButton() {

    const [clicked, setClicked] = useState(false);
    const [portfolios, setPortfolios] = useState([])

    useEffect(() => axios.get(urlGetPortofolios, headersWithoutCookies)
    .then(res => {
            setPortfolios(res.data.result)
    })
    .catch(error => console.log(error)), [])


    const handleClick = () => {
        setClicked(!clicked)     
    };

    useEffect(() => {
        if (clicked === true) {
            $("#portfolio_list").show()
        } else {
            $("#portfolio_list").hide()
        }
    }, [clicked])

    return (
        <>
       
            <Button 
            className = "ui pointing dropdown link item"
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
     
    
        {    (portfolios.length > 0) &&

                
                (<div className="ui raised fluid text segment" id="portfolio_list" > 
                    <div class="menu">
                    <div class="header">Portfolios</div>         
    
       { 
       portfolios.map((portfolio, index) => 
       <>
       <div className= 'item'> <Portfolio key={index} portfolio={portfolio} /></div>
       <div class="divider" id= {index}></div>
       </>)
       
       }

        </div>
        </div>)

       

        }
         
        </>

    
    )
}
