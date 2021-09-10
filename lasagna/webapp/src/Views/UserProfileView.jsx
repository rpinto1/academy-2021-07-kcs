import React, { useState, useEffect } from 'react'
import UserHeader from '../components/UserHeader';
import { Container, Table, Tab, Dropdown, Label, Menu, Icon } from 'semantic-ui-react'
import DrawGraph from '../components/UserProfile/DrawGraph';
import data from "./testData/data.json";


export default function UserProfileView() {

    /*graph test*/
    const testData =   [{ year: '1990', summary: 1000, cashflow: 150, debt: 100, dividends: 1000, eps: 250 , sales: -470 },
                    { year: '1991', summary: 531, cashflow: 1150, debt: 350, dividends: 750, eps: 620 , sales: -275 },
                    { year: '1992', summary: 1000, cashflow: 150, debt: 100, dividends: 1000, eps: 250 , sales: -15 },
                    { year: '1993', summary: 2200, cashflow: 120, debt: 200, dividends: 700, eps: 500 , sales: 150 },];

  


    const [activePortfolio, setActivePortfolio] = useState(0);

    const [activeCompany, setActiveCompany] = useState(0);


    const dataNames = data.map((item, i) => {
        return { index: i, text: item.portfolioName, value: i }
    });

    const panes = [
        { menuItem: 'Summary', render: () => <Tab.Pane> <DrawGraph indicator={data[activePortfolio]["portfolioCompanies"][activeCompany]["values"]} dataKey="summary" /> </Tab.Pane> },
        { menuItem: 'Cashflow', render: () => <Tab.Pane> <DrawGraph indicator={data[activePortfolio]["portfolioCompanies"][activeCompany]["values"]} dataKey="cashflow" /> </Tab.Pane> },
        { menuItem: 'Debt', render: () => <Tab.Pane><DrawGraph indicator={data[activePortfolio]["portfolioCompanies"][activeCompany]["values"]} dataKey="debt" /></Tab.Pane> },
        { menuItem: 'Dividends', render: () => <Tab.Pane><DrawGraph indicator={data[activePortfolio]["portfolioCompanies"][activeCompany]["values"]} dataKey="dividends" /></Tab.Pane> },
        { menuItem: 'EPS', render: () => <Tab.Pane><DrawGraph indicator={data[activePortfolio]["portfolioCompanies"][activeCompany]["values"]} dataKey="eps" /></Tab.Pane> },
        { menuItem: 'Sales', render: () => <Tab.Pane><DrawGraph indicator={data[activePortfolio]["portfolioCompanies"][activeCompany]["values"]} dataKey="sales" /></Tab.Pane> },
    ]


    const handlePortfolioChange = (e, {value}) => {
        
        setActivePortfolio(value);
        setActiveCompany(0);
    };


    const handleCompanyChange = (e, {index}) => {
        setActiveCompany(index);
    };


    const PortfolioDetails = () => <Tab panes={panes} />


    const PortfolioDropdown = () => (
        <Dropdown
            placeholder='Select Portfolio'
            fluid
            selection
            clearable
            options={dataNames}
            onChange={handlePortfolioChange}
        />
    );

    const PortfolioCompanies = () => {
        return (
            <>
                <Menu secondary vertical>
                    {
                        
                        data[activePortfolio]["portfolioCompanies"].map((item, i) => {
                            return (
                                <Menu.Item
                                    name={(item.name)}
                                    active={activeCompany === i}
                                    key={i}
                                    index={i}
                                    onClick={handleCompanyChange}
                                />
                            )

                        })
                    }
                </Menu>
            </>
        );
    }

    


    return (

        <div>
            <UserHeader />

            <Container className="profile">

                <section className="greeting">
                    <article className="avatar">
                        <img src="../blank-avatar-sm.jpg" alt="" />
                    </article>

                    <article>
                        <h1>Hello, (UserName)!</h1>
                        <a href="http://localhost:3010/user/edit">Edit my profile</a>
                    </article>
                </section>

                <section className="portfolio-section">

                    <section className="portfolio-list">

                        <PortfolioDropdown />

                        <PortfolioCompanies />

                        <hr />

                        <a href="http://localhost:3010/user/edit/portfolio">Edit portfolio</a>

                    </section> {/*end portfolio list*/}

                    <section className="portfolio-item-detail">

                        <PortfolioDetails className="detail-table" />

                    </section>

                </section>{/*end portfolio section*/}

            </Container>

        </div>
    )
}
