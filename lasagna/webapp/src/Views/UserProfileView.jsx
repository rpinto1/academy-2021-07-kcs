import React, { useState, useEffect } from 'react'
import { Container, Dropdown, Menu, Input, Form, Message, Button } from 'semantic-ui-react'
//import data from "./testData/data.json";
import { Link } from 'react-router-dom';
import UserHeader from '../components/UserHeader';
import UserGreeting from '../components/UserProfile/UserGreeting';
import PortfolioDetails from '../components/UserProfile/PortfolioDetails';
import Footer from '../components/Footer';
import { userId, token } from '../components/UserManager';


export default function UserProfileView() {

    const [data, setData] = useState([]);
    const [activePortfolio, setActivePortfolio] = useState(0);
    const [activeCompany, setActiveCompany] = useState(0);
    const [activeCompanyName, setActiveCompanyName] = useState('');
    const [activeCompanyTicker, setActiveCompanyTicker] = useState('');

    const [score, setScore] = useState(0.0);

    const [activeCompanyResponse, setActiveCompanyResponse] = useState([]);
    const [activeCompanyValues, setActiveCompanyValues] = useState([]);

    const [finishedLoading, setFinishedLoading] = useState(false)
    const [noPortfolioInfo, setNoPortfolioInfo] = useState(false);
    const [noCompanies, setNoCompanies] = useState(false);

    const [userName, setUserName] = useState("")


    const userId = localStorage.getItem("id");

    const url = `http://localhost:3010/api/Portfolios/portfolio?userId=${userId}`;



    const fetchAndSet = (url, setterFunc, setterFuncs, index) => {

        setFinishedLoading(false);

        (async function () {
            try {
                const response = await fetch(url)

                const data = await response.json();

                if (data != null) {
                    setterFunc(() => data.result);
                    setFinishedLoading(true);

                    if(setterFuncs && index) {
                        for(let func in setterFuncs){
                            func(data.result[index]);
                        }
                    }

                }

            } catch (e) {
                console.error("Error while fetching: ", e);
            }
        })()
    }



    useEffect(() => {

        (() => {
            fetchAndSet(url, setData);

            if (data.length === 0) {

                setNoPortfolioInfo(true);
            } else if (data["values"][activePortfolio].portfolioCompanies.length === 0) {
                setNoCompanies(true);
            }

        })();

    }, []);



    const handlePortfolioChange = (e, { value }) => {
        setActivePortfolio(value);
        setActiveCompany(0);

        if(data[activePortfolio].portfolioCompanies.length > 0) handleCompanyChange(null, 0);
    };


    const handleCompanyChange = (e, { index }) => {
        setActiveCompany(index);
        console.log('index ', index);

        setActiveCompanyValues([]);


        (async () => {
            fetchAndSet(`http://localhost:3010/api/Portfolios/portfolioCompanyValues/?ticker=${data[activePortfolio].portfolioCompanies[activeCompany].ticker}`, setActiveCompanyResponse, []);
            setScore(() => activeCompanyResponse["score"]);
            setActiveCompanyValues(() => activeCompanyResponse["values"]);
            setActiveCompanyName(() => activeCompanyResponse["name"]);
            setActiveCompanyTicker(() => activeCompanyResponse["ticker"]);

        })()

    };


    const dropdownOptions = () => {

        if (data.length > 0) {
            let options = data.map((item, i) => {
                return { index: i, text: item.portfolioName, value: i };
            });
            return options;
        }

        return [{ index: 0, text: 'Loading data...', value: 0, disabled: true }];

    }


    const Greeting = () => {
        return (
            <section className="greeting">
                <article className="avatar">
                    <img src="../blank-avatar-sm.jpg" alt="" />
                </article>

                <article>
                    <h1>Hello{userName === "" ? "!" : `, ${userName}!`}</h1>
                    <Link to='/user/profile/edit'>Edit my profile</Link>
                </article>
            </section>
        );
    }




    const PortfolioDropdown = () => (
        <Dropdown
            placeholder={data.length > 0 ? 'Select Portfolio' : (noPortfolioInfo ? 'No Portfolios' : 'Loading data...')}
            fluid
            selection
            loading={!finishedLoading}
            options={dropdownOptions()}
            onChange={handlePortfolioChange}

        />
    );


    const PortfolioCompanies = () => (
        <Menu secondary vertical>
            {data.length > 0

                ?
                data[activePortfolio].portfolioCompanies.length > 0

                    ?
                    data[activePortfolio].portfolioCompanies.map((item, i) => {
                        return (
                            <Menu.Item
                                name={`${item.ticker} ${item.name}`}
                                content={`${item.ticker} | ${item.name}`}
                                active={activeCompany === i}
                                key={i}
                                index={i}
                                onClick={handleCompanyChange} />
                        );
                    })

                    :
                    <Menu.Item
                        name={finishedLoading ? "No Companies" : "Loading..."}
                        active={true}
                        index={1}
                    />

                :
                <Menu.Item
                    name={noPortfolioInfo ? "No Portfolios" : "Loading..."}
                    active={true}
                    index={1}
                />}
        </Menu>
    );




    const createNewPortfolio = async (e) => {

        const requestBody = {
            name: e.target[0].value,
            userId: userId
        }

        const options = {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestBody)
        }


        let request = await fetch("http://localhost:3010/api/Portfolios/createPortfolio", options)

        if (request.ok) {
            await fetchAndSet(url, setData);
            setFinishedLoading(true);
        }


    };


    const AddPortfolio = () => {

        const [formField, setFormField] = useState("");

        const handleSubmit = (e) => {
            createNewPortfolio(e);
            setFormField("");
            setFinishedLoading(false);
        };

        return (
            <Form onSubmit={handleSubmit} loading={!finishedLoading}>
                <Form.Field>
                    <input
                        type="text"
                        placeholder="New Portfolio Name"
                        disabled={!finishedLoading}
                        value={formField}
                        onChange={({ target: { value } }) => setFormField(value)}
                    />
                </Form.Field>
                <Button>Submit</Button>
            </Form>
        );
    }


    return (

        <div>
            <UserHeader setUserName={setUserName} />
            <Container className="profile">

                <UserGreeting />

                <section className="portfolio-section five-vw-margin-lr">
                    <section className="portfolio-list">

                        <PortfolioDropdown />

                        <PortfolioCompanies data={data} />

                        <hr />
                        {
                            data.length > 0 && (
                                <>
                                    <a href={`http://localhost:3000/user/portfolio/edit/${data[activePortfolio].portfolioId}`}>Edit portfolio</a>
                                    <hr />
                                </>)
                        }

                        <AddPortfolio />

                    </section>

                    <section className="portfolio-item-detail">
                        <PortfolioDetails 
                        score={score} 
                        data={activeCompanyValues} 
                        name={activeCompanyName} 
                        ticker={activeCompanyTicker}
                        className="detail-table" />
                    </section>

                </section>

            </Container>

            <Footer />

        </div>
    )
}
