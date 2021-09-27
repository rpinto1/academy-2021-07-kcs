import React, { useState } from 'react'
import DrawGraph from './DrawGraph';
import { Tab, Placeholder, Header } from 'semantic-ui-react'

export default function PortfolioDetails({ data, score, ticker, loading }) {



    
    
    const Graph = ({ dataKey }) => {
        return data != null
            ? (
                data.length > 0

                    ? (<DrawGraph data={data} dataKey={dataKey} />)
                    : (<p>No Company data</p>)

            )
            : (
                <Placeholder fluid>
                    <Placeholder.Header image>
                        <Placeholder.Line />
                        <Placeholder.Line />
                    </Placeholder.Header>
                    <Placeholder.Paragraph>
                        <Placeholder.Line />
                        <Placeholder.Line />
                        <Placeholder.Line />
                        <Placeholder.Line />
                    </Placeholder.Paragraph>
                </Placeholder>
            );
    }




    const panes = [
        { menuItem: 'Summary', render: () => (
            <Tab.Pane> 
                { loading ? <h3>Loading...</h3> : (ticker !== '' &&  <h3>{`${ticker}`}</h3>)}
                {data.length > 0 ? <h1> Score: <span className={score < 0 ? 'negative' : 'positive'}>{Number(score).toFixed(2)}%</span> </h1> : <h1>No data</h1>}
            </Tab.Pane>
        )},
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
