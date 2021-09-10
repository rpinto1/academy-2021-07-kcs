import React from 'react';
import Body from '../components/Body';
import Footer from '../components/Footer';
import Header from '../components/Header';


export default function HomepageGuest() {
    return (
        <div className= 'everything'>
            <div>
                <Header />
                <Body />
            </div>

            <div>
                <Footer />
            </div>
        </div>
    )
}