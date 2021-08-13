import React from 'react';
import ArticleList from './News component/ArticleList';
import { Link, Route } from 'react-router-dom';
import AboutUsView from '../Views/AboutUsView';
import ContactUsView from '../Views/ContactUsView';

export default function Footer() {
    return (
        <footer>
            <ArticleList />

            <section className="company-info">
                <div className='ui celled horizontal list'>
                    <div className='item'><Link to='/aboutus'>About us</Link></div>
                    <div className='item'> <Link to='/contactus'>Contact</Link></div>
                </div>
            </section>

            
            <Route path='/aboutus' component={AboutUsView} />
            <Route path='/bookdetails/:id' component={ContactUsView} />
        </footer>
    )
}