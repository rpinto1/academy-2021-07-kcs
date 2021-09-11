import React from 'react';


function Article({ article }) {

    const { title, url } = article;

    return (

        <section className='news-container'>
        

            <a href={url} target='_blank' id='underlined'> {title}</a>
        </section>

    );
}

export default Article;