import React from 'react';
import UserHeader from '../components/UserHeader';
import { cookiesArray } from '../components/UserManager';
import Body from '../components/Body';
import Footer from '../components/Footer';

export default function HomepageUser() {
    

    return (
        <div className= 'everything'>
                    <div>
            <UserHeader />
           <h1> User HomePage </h1>
           <p>minha cookie é: {cookiesArray} </p>
           <Body />
         
        </div>

        <div>
            <Footer />
        </div>
    </div>
    )
}


