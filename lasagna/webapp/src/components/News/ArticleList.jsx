import React, { useState, useEffect } from 'react';
import Article from './Article';
import NoNewsError from './NoNewsError';

function ArticleList() {

    const [articles, setArticles] = useState([]);

    const [location, setLocation] = useState('us'); 

    const NEWS_API_KEY = "1d46c2ebccbd48e597c869e5881a2d87";

    useEffect(() => {
        const fetchArticles = async () => {
            try {
                const response = await fetch(
                `https://newsapi.org/v2/top-headlines?country=${location}&category=business&pageSize=5&apiKey=${NEWS_API_KEY}`
                );
                const json = await response.json();
                setArticles(json.articles);

            } catch (e) {
                <NoNewsError />
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

                            <td><Article article={article} key= {i}/></td>

                        ))
                    }
                </tr>
            </table>
           
      
    );
    
}

export default ArticleList;