import React, { useState, useEffect } from 'react';
import Article from './Article';


function ArticleList() {

    const [articles, setArticles] = useState([]);

    const [query, setQuery] = useState('stocks');

    const NEWS_API_KEY = "1d46c2ebccbd48e597c869e5881a2d87";

    useEffect(() => {
        const fetchArticles = async () => {
            try {
                const response = await fetch(
                    `https://newsapi.org/v2/everything?q=${query}&language=en&sortBy=publishedAt&pageSize=5&apiKey=${NEWS_API_KEY}`
                );
                const json = await response.json();
                setArticles(json.articles);

            } catch (e) {
                console.error(e);
            }
        }
        fetchArticles();

    }, [])




    return (
            <table className='ui table'>
                <tr><th id='news-title'>Latest News</th></tr>
                <tr>
                    {
                        articles &&
                        articles.map((article, i) => (

                            <td><Article article={article} /></td>

                        ))
                    }
                </tr>
            </table>
           
      
    );


}

export default ArticleList