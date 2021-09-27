import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom';
import axios from 'axios';
import { Container, Dropdown, Menu, Input, Form, Message, Button } from 'semantic-ui-react'

import UserHeader from '../components/UserHeader';
import UserGreeting from '../components/UserProfile/UserGreeting';
import PortfolioDetails from '../components/UserProfile/PortfolioDetails';
import Footer from '../components/Footer';
import { userId, token, headers } from '../components/UserManager';

import { portfolioAdd, portfolioDelete, portfolioAddBulk } from '../redux/portfoliosReducer';
import { useDispatch, useSelector } from 'react-redux'



export default function UserProfileView() {

    const data = useSelector(state => state.portfolios);
    console.log('data debug ', data);
    const dispatch = useDispatch();

    //const [data, setData] = useState([{ portfolioCompanies: [{ ticker: '' }] }]);

    //consider using an object with these two properties
    const [activePortfolio, setActivePortfolio] = useState(0);
    const [activeCompany, setActiveCompany] = useState(0);

    const [activeCompanyTicker, setActiveCompanyTicker] = useState('');

    const [score, setScore] = useState(0.0);

    const [activeCompanyValues, setActiveCompanyValues] = useState([]);

    const [finishedLoading, setFinishedLoading] = useState(false);
    const [noPortfolioInfo, setNoPortfolioInfo] = useState(false);
    const [noCompanies, setNoCompanies] = useState(false);

    const [userName, setUserName] = useState("")


    const url = `http://localhost:3010/api/Portfolios/portfolio?userId=${userId}`;


    const refreshPortfolios = () => {
        setFinishedLoading(false);

        


        if(data[0].portfolioCompanies[0].portfolioId === undefined){

            (() => axios.get(url, headers)
            .then(res => {

                console.log('Response - On mount: ', res.data.result);

                dispatch(portfolioAddBulk(res.data.result));
                
                console.log('redux data INSIDE refreshPf: ', data);

                if (res.data.result.length === 0) {
                    setNoPortfolioInfo(true);
                } else if (res.data.result["values"][activePortfolio] !== undefined) {
                    if(res.data.result["values"][activePortfolio].portfolioCompanies.length === 0) setNoCompanies(true);
                }

                setFinishedLoading(true);

            })
            .catch(error => console.log(error)))();
        }

       
    }

    console.log('redux data: ', data);

    useEffect(() => {

        refreshPortfolios();
    }, []);


    useEffect(() => {

        //reset values
        setActiveCompany(0);
        setActiveCompanyValues([]);
        setActiveCompanyTicker('');
        refreshPortfolios();
    }, [activePortfolio]);

    useEffect(() => {

        updateCompanyValues();
    }, [activeCompany]);


    const updateCompanyValues = () => {

        //reset values
        setActiveCompanyValues([]);
        setActiveCompanyTicker('');

        setFinishedLoading(false);

        if (data[activePortfolio].portfolioCompanies.length > 0) {

            console.log('indices ', activePortfolio, activeCompany);

            let ticker = 
                data[activePortfolio].portfolioCompanies[activeCompany].ticker
                //"MSFT:US"
            ;

            console.log('test ticker ', ticker);

            const companyDataEndpoint = `http://localhost:3010/api/Portfolios/portfolioCompanyValues/?ticker=${ticker}`;

            (() => axios.get(companyDataEndpoint, headers)
                .then(res => {

                    if (res.data) {

                        console.log('Response - Company change: ', res.data);

                        setFinishedLoading(true);
                        setScore(() => res.data.result["score"]);
                        setActiveCompanyValues(() => res.data.result["values"] || []);
                        setActiveCompanyTicker(() => res.data.result["ticker"] || 'No data');

                    }
                })
                .catch(error => console.log(error)))();
        }
    }


    

    const handlePortfolioChange = (e, { value }) => {

        setActivePortfolio(value);
        setActiveCompany(0);
        updateCompanyValues();
    };



    const handleCompanyChange = (e, { index }) => {

        setActiveCompany(index);
        console.log('company index ', index);
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


    /* const Greeting = () => {
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
    } */


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
            //refreshPortfolios();

            request
            .then(res => res.json())
            .then(data => dispatch(portfolioAdd(data)));
            
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
                            ticker={activeCompanyTicker}
                            loading={!finishedLoading}
                            className="detail-table" />
                    </section>

                </section>

            </Container>

            <Footer />

        </div>
    )
}
