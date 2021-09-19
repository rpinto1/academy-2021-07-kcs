import React from 'react';
import { Link } from 'react-router-dom';

export default function Logo() {
    return (
    
        <div className='logo'>
            <Link exact="true" to='/'> <img src='../logo.jpg' /> </Link>
        </div>
    
    )
}
