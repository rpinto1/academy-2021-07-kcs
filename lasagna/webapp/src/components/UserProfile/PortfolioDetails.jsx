import React from 'react'
import DrawGraph from './DrawGraph';
import { Tab } from 'semantic-ui-react'

export default function PortfolioDetails({ data, activeCompany, activePortfolio }) {
    
    const panes = [
        { menuItem: 'Summary', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="summary" /> </Tab.Pane> },
        { menuItem: 'Cashflow', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="cashflow" /> </Tab.Pane> },
        { menuItem: 'Debt', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="debt" /> </Tab.Pane> },
        { menuItem: 'Dividends', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="dividends" /> </Tab.Pane> },
        { menuItem: 'EPS', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="eps" /> </Tab.Pane> },
        { menuItem: 'Sales', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="sales" /> </Tab.Pane> },
    ];
    
    return (
        <Tab panes={panes} renderActiveOnly={true} className="five-vw-margin-lr" />
    )
}
