import React from 'react';


function Article({ article }) {

    const { title, url, urlToImage } = article;

    return (

        <section className='news-container'>
            {/*  <picture id = 'news-img'>
          <img src={urlToImage} />
          </picture>
           */}

            <a href={url} target='_blank' id='underlined'> {title}</a>
        </section>

    );
}

export default Article;