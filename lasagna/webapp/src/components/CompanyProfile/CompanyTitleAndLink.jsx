import React from 'react';
import { Header, Segment, Icon, Button, Container, Grid } from 'semantic-ui-react';
import AddToPortfolioButton from './AddToPortfolioButton';

export default function CompanyTitleAndLink({ companyInfo }) {

    const {name, ticker} = companyInfo;

    const baseUrl = `https://www.google.com/search?q="annual+report"+`;
    const fixedName = name.split(' ').map(word => word+"+").join('');
    const append = baseUrl+fixedName;


    return (
        <div>
            <Header as='h2' attached='top' >
                {name}
            </Header>
            <Segment attached>
                <Grid columns={2} relaxed='very'>
                    <Grid.Column>
                         Annual investors' page: <a href={append} >{ticker}</a>
                    </Grid.Column>
                    <Grid.Column>
                     <Button icon labelPosition ='left' size='tiny' floated='right'>
                             <Icon disabled name='bell outline' color='black'/>
                              Create alarm
                        </Button>
                     <AddToPortfolioButton ticker={ticker} />
                                   
                    </Grid.Column>
                </Grid>
            </Segment>
            
           
           


        </div>
    )
}
