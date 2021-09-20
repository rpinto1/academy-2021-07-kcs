import React, { useState, useEffect } from 'react'
import { Container, Dropdown, Menu, Input, Form, Message, Button } from 'semantic-ui-react'
//import data from "./testData/data.json";
import UserHeader from '../components/UserHeader';
import PortfolioDetails from '../components/UserProfile/PortfolioDetails';
import Footer from '../components/Footer';


export default function UserProfileView() {

    const [data, setData] = useState([]);

    const [activePortfolio, setActivePortfolio] = useState(0);

    const [activeCompany, setActiveCompany] = useState(0);

    const [newPortfolioName, setNewPortfolioName] = useState("");



    const userId = localStorage.getItem("id");

    const testUserId = "0753c920-cfe1-456c-a4c6-36de26ae40b8";


    const url = `http://localhost:3010/api/Companies/portfolio?userId=${testUserId}`;


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

    const handleNewPortfolioNameChange = ({ target: { value } }) => {
        setNewPortfolioName(value);
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
                    <a href="http://localhost:3010/user/edit">Edit my profile</a>
                </article>
            </section>
        );
    }


    const PortfolioDropdown = () => (
        <Dropdown
            placeholder={data.length > 0 ? 'Select Portfolio' : 'Loading data...'}
            fluid
            selection
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
                            index="1"
                        />
                }
            </Menu>
        );


    }

    const createNewPortfolio = (e) => {

        /* let request = fetch("localhost:3010/api/Companies/CreatePortfolio", { 
            method: "POST", 
            body: { 
                name: e.target.value, 
                userId
            } 
        }) */

        console.log();

    };


    const AddPortfolio = () => {

        return (
            <Form onSubmit={() => console.log(newPortfolioName)}>
                
                <Form.Field>
                    
                    <input
                        type="text"
                        placeholder="New Portfolio Name"
                        value={newPortfolioName}
                        onChange={({ target: { value } }) => setNewPortfolioName(value)}
                        id="EmailAddress"
                    />
                </Form.Field>
                <Button>Submit</Button>
            </Form>

        );

    }


    const PortfolioBody = () => {
        //if (data.length > 0) {
        return (
            <section className="portfolio-section five-vw-margin-lr">

                <section className="portfolio-list">


                    {
                        data.length > 0
                            ? (
                                <>
                                    <PortfolioDropdown />
                                    <PortfolioCompanies data={data} />
                                </>)
                            : (<h2>Loading data</h2>)

                    }
                    <PortfolioDropdown />

                    <PortfolioCompanies data={data} />

                    <hr />

                    {
                        data.length > 0 && (<a href={`http://localhost:3000/user/portfolio/edit/${data[activePortfolio].portfolioId}`}>Edit portfolio</a>)
                    }

                    <hr />

                    <AddPortfolio />
                    <p>New Name: {newPortfolioName}</p>

                </section> {/*end portfolio list*/}

                <section className="portfolio-item-detail">

                    <PortfolioDetails data={data} activeCompany={activeCompany} activePortfolio={activePortfolio} className="detail-table" />

                </section>

            </section>
        );

        //}

        /* return (
            <h1>Loading...</h1>
        ); */
    }



    return (

        <div>
            <UserHeader />

            <Container className="profile">

                <Greeting />

                <PortfolioBody />


            </Container>

            <Footer />

        </div>
    )
}
