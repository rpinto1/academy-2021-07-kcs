﻿import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'semantic-ui-react';


export default function Header() {
    return (
        <header>
            <div className="total-top">
                <div className='logo'>
                    <Link exact to='/'> <img src='../logo.jpg' /> </Link>
                </div>

                <div className='search-bar'>

                    <div class="ui disabled icon input">
                        <i class="search icon"></i>
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