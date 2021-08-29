import React, { useState, useEffect } from 'react';
function App() {
    const [articles, setArticles] = useState([]);
    const [company, setCompany] = useState('React');

    useEffect(() => {
        fetch("url").then(result => {
            if (result.status != 200) {
                console.log("error");
                return;
            }
            result.json().then(data => {
                if (data != null) {
                    setArticles(data.data.children);
                }
            })
        })
    }, [company]);

    return (
        <div className="App">
            <header className="App-header">
                <input type="text" className="input" value={company} onChange={test => setCompany(test.target.value)} />
            </header>
            <div className="articles">
                {
                    (articles != null) ? articles.map((article, index) => <Article key={index} article={article.data} />) : ''
                }
            </div>
        </div>
    );
}

export default App;
