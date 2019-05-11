import React, { Component } from 'react';
import ProductListing from '../productListing';
import SideMenu from '../../components/sideMenu';
import {Layout, Loading, Pagination} from 'element-react/next';
import * as ProductsDB from '../../DB/productsDB';
import * as CompaniesDB from '../../DB/companiesDB';
import * as cssClasses from './products.module.css';

class Products extends Component {
  constructor(props)
  {
    super(props);
    this.state = {
      companies: null,
      products: null,
      loading: true,
      total: 0,
      pageSize: 3,
      currentPage: 1
    }
    this.pageChanged = this.pageChanged.bind(this);
    this.getProducts = this.getProducts.bind(this);
  }

  getProducts = (currentPage) => {
    this.setState({loading: true});
    CompaniesDB.getAll()
    .then( res => this.setState({companies: res.data})
    );
    ProductsDB.getAll(this.state.pageSize, currentPage)
    .then( res => {
      const total = JSON.parse(res.headers["x-pagination"]).totalCount;
      this.setState({
        products: res.data,
        total
      });
      setTimeout(()=>{this.setState({loading:false})}, 1000);
    }
    );
  }
  componentDidMount()
  {
    this.getProducts(this.state.currentPage);
  }
  pageChanged = (currentPage) => {
    this.setState({currentPage});
    this.getProducts(currentPage);
  }
  render() {
    let productsTable = null;
    if(this.state.products) productsTable = (<ProductListing products={this.state.products}/>);
    return (
    <Loading loading={this.state.loading}>
      <Layout.Row gutter="0">
        <Layout.Col span="5">
          <SideMenu companies={this.state.companies}/>
        </Layout.Col>
        <Layout.Col span="19">
          {productsTable}
          <Pagination onCurrentChange={this.pageChanged} className={cssClasses.pagination}  layout="prev, pager, next" pageSize={this.state.pageSize} total={this.state.total} currentPage={this.state.currentPage}/>
        </Layout.Col>
      </Layout.Row>
    </Loading>
    );
  }
}

export default Products;
