import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'semantic-ui-react';
import Logo from './Logo';
import SearchBar from './SearchBar/SearchBar';


export default function Header() {
    return (
      
        <header>
             
            
            <div className="total-top">
                <Logo />

                <SearchBar />

                <div className='buttons'>

                    <Link to='/signin'> <button className='ui small right floated teal button'>Sign in</button> </Link>
               
                    <Link to='/signup'> <Button className='ui small right floated blue button'>Sign up</Button> </Link>

                </div>
            </div>

        </header>
      
    )
}
