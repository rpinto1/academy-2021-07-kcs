import React from 'react'
import DrawGraph from './DrawGraph';
import { Tab } from 'semantic-ui-react'

export default function PortfolioDetails({ data, activeCompany, activePortfolio }) {

    const Graph = ({ dataKey }) => {
        return data.length > 0
            ? (<DrawGraph data={data[activePortfolio].portfolioCompanies[activeCompany].values} dataKey={dataKey} />)
            : (<p>Loading...</p>);
    }


    const panes = [
        { menuItem: 'Summary', render: () => <Tab.Pane> Score data </Tab.Pane> },
        { menuItem: 'ROIC', render: () => <Tab.Pane> <Graph dataKey="roic" /> </Tab.Pane> },
        { menuItem: 'Equity', render: () => <Tab.Pane> <Graph dataKey="equity" /> </Tab.Pane> },
        { menuItem: 'EPS', render: () => <Tab.Pane> <Graph dataKey="eps" /> </Tab.Pane> },
        { menuItem: 'Sales', render: () => <Tab.Pane> <Graph dataKey="sales" /> </Tab.Pane> },
        { menuItem: 'Cash', render: () => <Tab.Pane> <Graph dataKey="cash" /> </Tab.Pane> },
    ];

    return (
        <Tab panes={panes} renderActiveOnly={true} className="five-vw-margin-lr" />
    )


}
