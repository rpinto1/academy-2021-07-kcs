import React from 'react';
import UserHeader from '../components/UserHeader';

export default function HomepageUser() {
    const hello = true;

    return (
        <div>
            <UserHeader />
           <h1> User HomePage </h1>
           {
               hello && sessionStorage.getItem('id')
           }
        </div>
    )
}


