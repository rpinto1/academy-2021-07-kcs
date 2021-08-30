import React from 'react';
import { Container } from 'semantic-ui-react';
import Logo from '../components/Logo';

export default function AboutUsView() {
    return (
        <>
        <div className="total-top">
            <Logo />
            <div></div>
            <div></div>
        </div>
        <Container className="our_info">
            <div className="total-top">
            <a href="https://kcsit.pt/" target='_blank'>
            <img src='../salesforceAcademytiny.png' />  
            </a>          
            </div>

            <h3>This is a project developed by KCS iT's Salesforce Academy 2021.
            It has been created based on the teachings of Phil Town's Rule #1 book. 

            Lasagna got its name from the way we want our code to work: layered. 
            Each bite brings all the flavors together.</h3>
            <br></br>
           <div  id="text-right">
            <p>Hopefully you will enjoy eating it as much as we enjoyed preparing it!</p>
            <p>Bruno, Joana, Pedro, Raúl, Ricardo, Rui, Vitor.  </p>
            </div>

        </Container>
        </>
    )
}
