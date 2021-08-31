import React from 'react';

import { Link} from 'react-router-dom';


export default function AboutUsContactButtons() {
    return (

        <section className="company-info">
            <div className='ui celled horizontal list'>
                <div className='item'><Link to='/aboutus'>About us</Link></div>
                <div className='item'> <Link to='/contactus'>Contact us</Link></div>
            </div>
        </section>
            
    
    )
}