import React from 'react';
import UserHeader from '../components/UserHeader';
import { userId } from '../components/UserManager';


export default function HomepageUser() {
    

    return (
        <div>
            
            <UserHeader />
           <h1> User HomePage </h1>
           <p>minha cookie é: {} </p>
         
        
         
        </div>
    )
}


