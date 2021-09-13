import React, { useState, useEffect } from 'react'
import { Container, Dropdown, Menu, Input } from 'semantic-ui-react'
import data from "./testData/data.json";
import UserHeader from '../components/UserHeader';
import PortfolioDetails from '../components/UserProfile/PortfolioDetails';
import Footer from '../components/Footer';


export default function UserProfileView() {


    const [activePortfolio, setActivePortfolio] = useState(0);

    const [activeCompany, setActiveCompany] = useState(0);


    const portfolioNames = data.map((item, i) => {
        return { index: i, text: item.portfolioName, value: i }
    });


    const handlePortfolioChange = (e, { value }) => {
        setActivePortfolio(value);
        setActiveCompany(0);
    };


    const handleCompanyChange = (e, { index }) => {
        setActiveCompany(index);
    };


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
            placeholder='Select Portfolio'
            fluid
            selection
            clearable
            options={portfolioNames}
            onChange={handlePortfolioChange}
        />
    );


    const PortfolioCompanies = () => {
        return (
            <Menu secondary vertical>
                {
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
                }
            </Menu>
        );
    }



    const AddPortfolio = () => {
        return <Input
            icon={{ name: 'plus', circular: true, link: true }}
            placeholder='Add Portfolio'
            onChange={() => console.log('test')} />;
    }



    return (

        <div>
            <UserHeader />

            <Container className="profile">

                <Greeting />

                <section className="portfolio-section five-vw-margin-lr">

                    <section className="portfolio-list">

                        <PortfolioDropdown />

                        <PortfolioCompanies data={data}/>

                        <hr />

                        <a href={`http://localhost:3010/user/edit/portfolio/${data[activePortfolio].guid}`}>Edit portfolio</a> {/* GUID? */}

                        <hr />

                        <AddPortfolio />

                    </section> {/*end portfolio list*/}

                    <section className="portfolio-item-detail">

                        <PortfolioDetails data={data} activeCompany={activeCompany} activePortfolio={activePortfolio} className="detail-table" />

                    </section>

                </section>{/*end portfolio section*/}

            </Container>

            <Footer />

        </div>
    )
}
