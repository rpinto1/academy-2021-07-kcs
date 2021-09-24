import React from 'react';
import UserHeader from '../components/UserHeader';
import { cookiesArray } from '../components/UserManager';

export default function HomepageUser() {
    

    return (
        <div>
            <UserHeader />
           <h1> User HomePage </h1>
           <p>minha cookie é: {cookiesArray} </p>
         
         
        </div>
    )
}


