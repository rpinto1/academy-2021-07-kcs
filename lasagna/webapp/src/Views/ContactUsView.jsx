import React from 'react';
import { Container } from 'semantic-ui-react';
import Logo from '../components/Logo';

export default function ContactUsView() {
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
            <div className="total-top">
                    <h1>Academia Salesforce 2021</h1>
            </div>
            <div className="total-top">
                <p>Rua Mestre do Campo 7 9760-498, Praia da Vitória Terceira - Portugal </p>
            </div>
            <div className="total-top">
                <p> info@kcsit.pt </p>
            </div>
        
        </Container>
        </>
    );
}