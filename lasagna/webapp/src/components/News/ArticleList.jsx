import React, { useState, useEffect } from 'react';
import Article from './Article';
import NoNewsError from './NoNewsError';

function ArticleList() {

    const [articles, setArticles] = useState([]);

    const [location, setLocation] = useState('us'); 


    useEffect(() => {
        const fetchArticles = async () => {
            try {
                const response = await fetch(`http://localhost:3010/api/ExternalServices/news`);
                const json = await response.json();
                const articles = json.result.articles;
                setArticles(articles);

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

                            <td key={i}><Article article={article}/></td>

                        ))
                    }
                </tr>
            </table>
           
      
    );
    
}

export default ArticleList;