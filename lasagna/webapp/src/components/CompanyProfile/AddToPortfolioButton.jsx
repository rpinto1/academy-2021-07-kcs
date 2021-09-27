import React, {useState} from 'react';
import axios from 'axios';
import { Button, Icon } from 'semantic-ui-react';
import { userId, headersWithoutCookies } from '../UserManager';
import { urlGetPortofolios } from '../PortfolioManager';


export default function AddToPortfolioButton() {

    const [clicked, setClicked] = useState(false);

    const handleClick = () => axios.get(urlGetPortofolios, headersWithoutCookies)
    .then(res => {
        console.log(res);
    })
    .catch(error => console.log(error));


    return (
        <Button 
        icon labelPosition ='left' 
        size='tiny' 
        floated='right'
        onClick = {handleClick}>
             
                                   
        {
            clicked && 
            <>
            <Icon 
                name='heart' 
                color='pink'/>
            
                 
                <p> hola</p>


            </>

        }
            <Icon 
                name='heart outline' 
                color='black'/>
                              Add to your portfolio
      
        </Button> 
    )
}
