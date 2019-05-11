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
      currentPage: 1,
      companyName: "All",
      searchQurey: ""
    }
    this.getProducts = this.getProducts.bind(this);
    this.pageChanged = this.pageChanged.bind(this);
    this.filterChanged = this.filterChanged.bind(this);
  }

  getProducts = (currentPage, companyName, searchQuery) => {
    this.setState({loading: true});
    ProductsDB.getAll(this.state.pageSize, currentPage, companyName, searchQuery)
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
    CompaniesDB.getAll()
    .then( res => this.setState({companies: res.data})
    );
  }
  pageChanged = (currentPage) => {
    this.setState({currentPage});
    this.getProducts(currentPage, this.state.companyName, this.state.searchQurey);
  }
  filterChanged = (index) => {
    this.getProducts(1,index, this.state.searchQurey);
    this.setState({
      currentPage: 1,
      companyName: index
    });
  }
  render() {
    let productsTable = null;
    if(this.state.products) productsTable = (<ProductListing products={this.state.products}/>);
    return (
    <Loading loading={this.state.loading}>
      <Layout.Row gutter="0">
        <Layout.Col span="5">
          <SideMenu onSelect={this.filterChanged} filter={this.state.companyName} companies={this.state.companies}/>
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
