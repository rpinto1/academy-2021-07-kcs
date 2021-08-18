import React from 'react';
import { Link, Route } from 'react-router-dom';
import { Button } from 'semantic-ui-react';
import Logo from './Logo';



export default function Header() {
    return (
      
        <header>
             
            
            <div className="total-top">
                <Logo />

                <div className='search-bar'>

                    <div className="ui disabled icon input">
                        <i className="search icon"></i>
                        <input type="text" placeholder="Search..." />
                    </div>

                </div>

                <div className='buttons'>

                    <Link to='/signin'> <button className='ui small right floated teal button'>Sign in</button> </Link>
               
                    <Link to='/createaccount'> <Button className='ui small right floated blue button'>Sign up</Button> </Link>

                </div>
            </div>

        </header>
      
    )
}
