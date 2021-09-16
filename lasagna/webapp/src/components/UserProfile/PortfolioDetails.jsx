import React from 'react'
import DrawGraph from './DrawGraph';
import { Tab } from 'semantic-ui-react'

export default function PortfolioDetails({ data, activeCompany, activePortfolio }) {
    
    
    if(data.length > 0){

        const panes = [
            { menuItem: 'Summary', render: () => <Tab.Pane> {/* <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="summary" /> */} Score data </Tab.Pane> },
            { menuItem: 'ROIC', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="roic" /> </Tab.Pane> },
            { menuItem: 'Equity', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="equity" /> </Tab.Pane> },
            { menuItem: 'EPS', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="eps" /> </Tab.Pane> },
            { menuItem: 'Sales', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="sales" /> </Tab.Pane> },
            { menuItem: 'Cash', render: () => <Tab.Pane> <DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey="cash" /> </Tab.Pane> },
        ];

        return (
            <Tab panes={panes} renderActiveOnly={true} className="five-vw-margin-lr" />
        )
    }


    return (
        <Tab renderActiveOnly={true} className="five-vw-margin-lr" />
    )
}
