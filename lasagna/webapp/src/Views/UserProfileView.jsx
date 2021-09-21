import React, { useState, useEffect } from 'react'
import { Container, Dropdown, Menu, Input, Form, Message, Button } from 'semantic-ui-react'
//import data from "./testData/data.json";
import { Link } from 'react-router-dom';
import UserHeader from '../components/UserHeader';
import PortfolioDetails from '../components/UserProfile/PortfolioDetails';
import Footer from '../components/Footer';


export default function UserProfileView() {

    const [data, setData] = useState([]);

    const [activePortfolio, setActivePortfolio] = useState(0);

    const [activeCompany, setActiveCompany] = useState(0);

    const [newPortfolioName, setNewPortfolioName] = useState("");



    const userId = localStorage.getItem("id");

    const token = localStorage.getItem("token");

    //const testUserId = "0753c920-cfe1-456c-a4c6-36de26ae40b8";


    const url = `http://localhost:3010/api/Companies/portfolio?userId=${userId}`;


    useEffect(() => {
        fetch(url).then(result => {
            if (result.status != 200) {
                console.log("error");
                return;
            }
            result.json().then(data => {
                if (data != null) {
                    setData(data.result);
                }
            })
        })
    }, []);


    const handlePortfolioChange = (e, { value }) => {
        setActivePortfolio(value);
        setActiveCompany(0);
    };


    const handleCompanyChange = (e, { index }) => {
        setActiveCompany(index);
    };


    const portfolioNames = data.map((item, i) => {
        return { index: i, text: item.portfolioName, value: i };
    });

    const Greeting = () => {
        return (
            <section className="greeting">
                <article className="avatar">
                    <img src="../blank-avatar-sm.jpg" alt="" />
                </article>

                <article>
                    <h1>Hello, (UserName)!</h1>
                    <Link to='/user/profile/edit'>Edit my profile</Link>
                </article>
            </section>
        );
    }


    const PortfolioDropdown = () => (
        <Dropdown
            placeholder={data.length > 0 ? 'Select Portfolio' : 'Loading data...'}
            fluid
            selection
            clearable
            options={data.length > 0 ? portfolioNames : [{ index: 0, text: 'Loading data...', value: 0 }]}
            onChange={handlePortfolioChange}
        />
    );


    const PortfolioCompanies = () => {

        return (
            <Menu secondary vertical>
                {
                    data.length > 0
                        ?
                        data[activePortfolio].portfolioCompanies.map((item, i) => {
                            return (
                                <Menu.Item
                                    name={(item.name)}
                                    active={activeCompany === i}
                                    key={i}
                                    index={i}
                                    onClick={handleCompanyChange} />
                            );
                        })

                        :
                        <Menu.Item
                            name="Loading..."
                            active="true"
                            index="0"
                        />
                }
            </Menu>
        );


    }

    const createNewPortfolio = (e) => {

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


        const request = fetch("localhost:3010/api/Companies/CreatePortfolio", options);

        //console.log(options);
    };


    const AddPortfolio = () => {

        const [formField, setFormField] = useState("");

        return (
            <Form onSubmit={createNewPortfolio}>

                <Form.Field>

                    <input
                        type="text"
                        placeholder="New Portfolio Name"
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
            <UserHeader />

            <Container className="profile">

                <Greeting />

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

                    <PortfolioDetails data={data} activeCompany={activeCompany} activePortfolio={activePortfolio} className="detail-table" />

                </section>

            </section>

            </Container>

            <Footer />

        </div>
    )
}
