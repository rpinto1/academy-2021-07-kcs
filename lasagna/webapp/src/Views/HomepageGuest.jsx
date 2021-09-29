import React, { useEffect, useState } from 'react';
import Body from '../components/Body';
import Footer from '../components/Footer';
import Header from '../components/Header';
import UserHeader from '../components/UserHeader';
import { userId } from '../components/UserManager';



export default function HomepageGuest() {

const [logged, setLogged] = useState(false);

useEffect(() => {
    if (userId) {
        setLogged(true);
        
    } else {setLogged(false)}}, [])
 
    return (
        <div className= 'everything'>
            <div>
        {
            logged && <UserHeader />
        }

        {
            !logged && <Header />
        } 
          
    
                <Body />
            </div>

            <div>
                <Footer />
            </div>
        </div>
    )
}