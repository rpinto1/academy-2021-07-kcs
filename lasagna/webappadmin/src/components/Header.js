import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'semantic-ui-react';
import Logo from './Logo';


export default function Header() {
    return (
      
        <header>
             
            <div className="total-top">
                <Logo />


                <div className='buttons'>

                    <Link to='/signin'> <Button className='ui small right floated teal button'>Sign in </Button> </Link>
               
                </div>
            </div>

        </header>
      
    )
}
